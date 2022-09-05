using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Services.Store;

namespace WPF_SilentUpdate
{
    class StoreManager
    {
        public static void CheckUpdateAndInstall()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(65 * 1000);
                        await SilentDownloadAndInstallUpdatesAsync();
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            });
        }


        public static async Task<bool> SilentDownloadAndInstallUpdatesAsync(bool preDownload = false)
        {

            StoreContext context = StoreContext.GetDefault();
            if (context.CanSilentlyDownloadStorePackageUpdates == false)
            {
                return false;
            }

            IReadOnlyList<StorePackageUpdate> storePackageUpdates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
            if (storePackageUpdates.Count == 0)
            {
                return false;
            }

            if (preDownload)
            {
                StorePackageUpdateResult downloadResult = await context.TrySilentDownloadStorePackageUpdatesAsync(storePackageUpdates);
                if (downloadResult.OverallState != StorePackageUpdateState.Completed)
                {
                    return false;
                }
            }

            StorePackageUpdateResult installResult = await context.TrySilentDownloadAndInstallStorePackageUpdatesAsync(storePackageUpdates);
            if (installResult.OverallState != StorePackageUpdateState.Completed)
            {
                return false;
            }
            
            System.Environment.Exit(1);
            return true;
        }
    }
}
