# DESCRIPTION
This project is a collection of small and useful tools that we wrote and write to get our daily development and administation work done.

# TOOLS

## HostsEditor

    Adds and removes entries to and from the Windows hosts file.
    Automatically backs up the original hosts file to hosts.bck.
    Does not add duplicate entries; instead prints a warning.

    Usage:
    ------
    Add:    HostsEditor.exe <ip-address> <host-name> [/q]
                            e.g.: HostsEditor.exe 127.0.0.1 my-local-site.com
    Remove: HostsEditor.exe <host-name> [/q]
                            e.g.: HostsEditor.exe my-local-site.com
    
    Options:
    --------
    /q      suppresses all regular output messages, but still shows exceptions
            has to be the last parameter
