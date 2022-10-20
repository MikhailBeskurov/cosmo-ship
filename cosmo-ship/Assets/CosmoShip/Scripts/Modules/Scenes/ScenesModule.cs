using System;
using System.Threading;
using System.Threading.Tasks;
using CosmoShip.Scripts.Models.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

namespace CosmoShip.Scripts.Modules.Scenes
{
    public interface IScenesModule
    {
        void LoadNewScene(ScenesData scenesData);
    }

    public class ScenesModule : IScenesModule
    {
        private CancellationTokenSource cts;

        public async void LoadNewScene(ScenesData scenesData)
        {
            if (cts == null)
            {
                cts = new CancellationTokenSource();
                try
                {
                    await PerformSceneLoading(cts.Token,scenesData.ToString());
                }
                catch (OperationCanceledException ex)
                {
                    if (ex.CancellationToken == cts.Token)
                    {
                        Debug.Log($"Scene ({scenesData.ToString()}) loaded!");                        
                    }
                }
                finally
                {
                    cts.Cancel();
                    cts = null;
                }
            }
            else
            {
                cts.Cancel();
                cts = null;
            }
        }
        
        //Actual Scene loading
        private async Task PerformSceneLoading(CancellationToken token,string sceneName)
        {
            token.ThrowIfCancellationRequested();
            if (token.IsCancellationRequested)
                return;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            while (true)
            {
                token.ThrowIfCancellationRequested();
                if (token.IsCancellationRequested)
                    return;               
                if (asyncOperation.progress>=0.9f)
                    break;               
            }
            asyncOperation.allowSceneActivation = true;            
            cts.Cancel();
            token.ThrowIfCancellationRequested();

            //added this as a failsafe unnecessary
            if (token.IsCancellationRequested)
                return;
        }
    }
}