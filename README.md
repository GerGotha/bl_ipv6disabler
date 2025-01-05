# bl_ipv6disabler

- Official Taleworlds documentation
https://moddocs.bannerlord.com/multiplayer/hosting_server/

This server is used only serverside on linux machines. It checks on startup if the IPv6 option in the runtime configuration is disabled and if not disables it and shutsdown the server again.

- Installation
The installation is quiete simple. Just download the zip directory or build the project on your own. Extract the WebServerInfo directory into your `Mount & Blade II Dedicated Server/Modules` folder.

After that add the `IPv6Disabler` module to your server startup at the end. Example: `_MODULES_*Native*Multiplayer*IPv6Disabler*_MODULES_`

If you build the project on your own, do not forget to add `MB_SERVER_PATH` environment variable. `MB_SERVER_PATH` should point to your `Mount & Blade II Dedicated Server` directory.

