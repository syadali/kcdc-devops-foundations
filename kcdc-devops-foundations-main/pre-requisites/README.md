# Workshop Prerequisites

**DevOps Foundations: From Code Commit to Production Confidence** · KCDC · 4-hour hands-on workshop

This is a hands-on workshop: you will take a real application from first commit to production on
Azure using GitHub, GitHub Actions, and Terraform. To participate fully, please complete **all**
of the setup below **before arriving** — conference wifi is shared, and the installers below are
sizeable. Setup takes about **30–45 minutes**.

> If you skip setup, you can still follow along, but you won't be able to do the labs hands-on.

---

## Equipment

- [ ] A laptop — Windows, macOS, or Linux all work — and a charger.
- [ ] **Admin access** on the machine to install the tools below. If your work laptop is locked
  down, a personal laptop is strongly recommended.

## VPN / Firewall

Corporate VPNs and firewalls frequently block the endpoints this workshop depends on:

`github.com` · `*.azurewebsites.net` · `login.microsoftonline.com` · `management.azure.com`

Please test the [verification commands](#verify-before-you-arrive) below while **off** your
corporate VPN. If your machine forces a VPN or blocks the Azure CLI,
[Azure Cloud Shell](https://shell.azure.com) (runs in a browser) is a workable fallback for the
Azure portions of the labs.

## Accounts (free)

| Account | Details |
|---|---|
| **GitHub** | Free tier is fine → [github.com/signup](https://github.com/signup) |
| **Azure subscription** | A [free account](https://azure.microsoft.com/free) or Visual Studio subscription credits. You need permission to create **resource groups** and an **Entra ID app registration** — a personal/free subscription has this by default; a company subscription may not. |

> 💰 **Cost:** total Azure spend during the workshop is **under $1** — one small App Service plan
> for ~3 hours, destroyed at the end of the session (we do the cleanup together).

## Software

Versions listed are what the workshop was built and tested with (verified August 2026).

### Required

| Tool | Version | Download |
|---|---|---|
| Git | 2.47+ (latest: 2.55) | [git-scm.com/downloads](https://git-scm.com/downloads) |
| Visual Studio Code | latest stable | [code.visualstudio.com](https://code.visualstudio.com) |
| Azure CLI | 2.80+ (current: 2.89) | [Install guide](https://learn.microsoft.com/cli/azure/install-azure-cli) |

### Optional

All builds and deployments run in GitHub Actions, so these are only for local experimenting:

| Tool | Version | Download |
|---|---|---|
| .NET SDK | 10.0 (LTS) | [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) |
| Terraform | 1.9+ | [Install guide](https://developer.hashicorp.com/terraform/install) |

## Verify before you arrive

All three must succeed:

```bash
git --version
az --version
az login          # opens a browser; sign in
az account show   # confirm it displays the subscription you'll use
```

## Knowledge

- Comfort with a command line/terminal and basic Git (clone, commit, push). The workshop includes
  a short refresher, but you'll move faster if these aren't brand new.
- **No prior GitHub Actions, Terraform, or Azure experience required** — that's what the workshop
  teaches.

## On the day

- A link to fork the workshop repository will be shared at the start of the session — no large
  downloads happen on site.
- Workshop seating is first come, first served.
- No Azure subscription? Labs 1–2 are GitHub-only, and you can pair with a neighbor for Labs 3–4.

---

*Questions before the conference? Open an issue in this repository.*
