using Panacea.Modularity.UiManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panacea.Modularity.WebBrowsing;
using Panacea.Core;
using Panacea.Modularity.UserAccount;

namespace Panacea.Modules.SkypeWeb
{
    public class SkypeWebPlugin : ICallablePlugin
    {
        private readonly PanaceaServices _core;

        public SkypeWebPlugin(PanaceaServices core)
        {
            _core = core;
        }

        public Task BeginInit()
        {
            return Task.CompletedTask;
        }

        public async void Call()
        {

            if (_core.TryGetUserAccountManager(out IUserAccountManager user))
            {
                if (await user.RequestLoginAsync("Please login to use Skype."))
                {
                    OpenSkypeWeb();
                }
            }
            else
            {
                OpenSkypeWeb();
            }

        }

        private void OpenSkypeWeb()
        {
            if (_core.TryGetWebBrowser(out IWebBrowserPlugin web))
            {
                web.OpenUnmanaged("https://web.skype.com",
                            true,
                            new List<string>()
                            {
                                "web.skype.com",
                                "lw.skype.com",
                                "login.live.com",
                                "login.skype.com",
                                "signup.live.com"
                            });
            }
        }

        public void Dispose()
        {

        }

        public Task EndInit()
        {
            return Task.CompletedTask;
        }

        public Task Shutdown()
        {
            return Task.CompletedTask;
        }
    }
}
