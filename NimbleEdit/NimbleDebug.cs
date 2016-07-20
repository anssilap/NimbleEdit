//------------------------------------------------------------------------------
// <copyright file="NimbleDebug.cs" company="CosyCave">
//     Copyright (c) CosyCave.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using System.Windows.Forms;

namespace NimbleEdit
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class NimbleDebug
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;
        private DTE dte;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("b6eb2312-49e1-4fc7-a633-3c78bab58319");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="NimbleDebug"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private NimbleDebug(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
            dte = (DTE)ServiceProvider.GetService(typeof(DTE));
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static NimbleDebug Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new NimbleDebug(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            if (dte == null)
            {
                MessageBox.Show("Failed to initialize DTE object.", "NimbleDebug", MessageBoxButtons.OK);
                return;
            }

            switch (dte.Debugger.CurrentMode)
            {
                case dbgDebugMode.dbgBreakMode:
                    dte.Debugger.Go(false);
                    break;
                case dbgDebugMode.dbgDesignMode:
                    dte.Debugger.Go(false);
                    break;
                case dbgDebugMode.dbgRunMode:
                    Document currentDocument = dte.ActiveDocument;
                    dte.Debugger.Break(true);
                    currentDocument.Activate();
                    break;
                default:
                    throw new NotSupportedException("Invalid debugger mode");
                    break;
            }
        }
    }
}
