using QuoteBoard.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<QuoteProvider>();
builder.Services.AddApplicationInsightsTelemetry();
var app = builder.Build();

// Chaos middleware: FAIL_RATE (0..1) randomly fails the homepage and the quotes API.
// /health is deliberately never affected — probes and the page's health dot stay honest.
app.Use(async (ctx, next) =>
{
    var rate = double.TryParse(app.Configuration["FAIL_RATE"], out var r) ? r : 0;
    var chaosPath = ctx.Request.Path == "/" || ctx.Request.Path.StartsWithSegments("/api/quotes");
    if (chaosPath && Random.Shared.NextDouble() < rate)
        throw new InvalidOperationException("Chaos monkey strikes! (FAIL_RATE is set)");
    await next();
});

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/api/quotes", (QuoteProvider q) => q.GetAll());

app.MapGet("/", (QuoteProvider q, IConfiguration cfg) =>
{
    var banner = cfg.GetValue<bool>("FEATURE_NEW_BANNER")
        ? "<div class='banner'>🚀 New feature released — with a flag flip, not a deploy!</div>"
        : "";
    var instance = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID")?[..8] ?? "local";
    var slot = Environment.GetEnvironmentVariable("WEBSITE_SLOT_NAME") ?? "local";
    var quote = q.GetDailyQuote(DateOnly.FromDateTime(DateTime.UtcNow));
    return Results.Content($$"""
<!doctype html>
<html><head><meta charset="utf-8"><title>QuoteBoard · KCDC 2026</title>
<style>
  body{font-family:system-ui;max-width:680px;margin:48px auto;padding:0 16px;color:#1f2328}
  .banner{background:#2da44e;color:#fff;padding:12px 16px;border-radius:8px;margin-bottom:20px}
  h1{margin:0 0 4px}
  .sub{color:#57606a;margin:0 0 28px}
  .card{border:1px solid #d0d7de;border-radius:12px;padding:24px;margin-bottom:20px}
  #quote{font-size:1.25em;min-height:3em}
  button{background:#2da44e;color:#fff;border:0;border-radius:8px;padding:10px 18px;font-size:1em;cursor:pointer}
  button:hover{background:#1a7f37}
  .meta{color:#57606a;font-size:.9em;display:flex;gap:18px;align-items:center;flex-wrap:wrap}
  .dot{display:inline-block;width:10px;height:10px;border-radius:50%;background:#d0d7de;margin-right:6px}
  .err{color:#cf222e}
</style></head>
<body>
{{banner}}
<h1>📋 Welcome to KCDC 2026</h1>
<p class="sub">DevOps Foundations workshop — from code commit to production confidence</p>
<div class="card">
  <p id="quote">“{{quote}}”</p>
  <button onclick="newQuote()">Another quote ↻</button>
</div>
<p class="meta">
  <span><span class="dot" id="dot"></span><span id="health">checking…</span></span>
  <span>Slot: <b>{{slot}}</b></span>
  <span>Instance: {{instance}}</span>
  <span><a href="/api/quotes">API</a> · <a href="/health">Health</a></span>
</p>
<script>
async function newQuote() {
  const el = document.getElementById('quote');
  try {
    const res = await fetch('/api/quotes');
    if (!res.ok) throw new Error('HTTP ' + res.status);
    const quotes = await res.json();
    el.textContent = '“' + quotes[Math.floor(Math.random() * quotes.length)] + '”';
    el.classList.remove('err');
  } catch (e) {
    el.textContent = '💥 ' + e.message + ' — is FAIL_RATE set? Check App Insights!';
    el.classList.add('err');
  }
}
async function ping() {
  const dot = document.getElementById('dot'), t = document.getElementById('health');
  const start = performance.now();
  try {
    const res = await fetch('/health');
    dot.style.background = res.ok ? '#2da44e' : '#cf222e';
    t.textContent = res.ok ? 'healthy · ' + Math.round(performance.now() - start) + ' ms' : 'unhealthy';
  } catch { dot.style.background = '#cf222e'; t.textContent = 'unreachable'; }
}
ping(); setInterval(ping, 5000);
</script>
</body></html>
""", "text/html; charset=utf-8");
});

app.Run();
