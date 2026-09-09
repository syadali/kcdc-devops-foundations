# Snippets for Lab 3.6 and demo prep — copy into infra/main.tf (don't apply this file directly)

# --- Tags (Lab 3.6): add inside azurerm_resource_group.app, re-run the pipeline,
#     and watch the plan report "1 to change" ---
# tags = {
#   workshop = "kcdc"
#   owner    = var.suffix
# }

# --- Email action group for the 5xx alert (optional demo upgrade):
#     the alert fires in the portal either way; this makes it also send an email ---
# resource "azurerm_monitor_action_group" "oncall" {
#   name                = "ag-quoteboard-${var.suffix}"
#   resource_group_name = azurerm_resource_group.app.name
#   short_name          = "quoteboard"
#
#   email_receiver {
#     name          = "oncall"
#     email_address = "you@example.com" # your real address
#   }
# }
#
# ...and inside azurerm_monitor_metric_alert.http5xx:
# action {
#   action_group_id = azurerm_monitor_action_group.oncall.id
# }
