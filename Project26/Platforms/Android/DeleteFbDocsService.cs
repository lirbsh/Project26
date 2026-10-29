using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Plugin.CloudFirestore;
using Project26.General;
using Project26.Models;
using Project26.ModelsLogic;

namespace Project26.Platforms.Android
{
    [Service]
    public class DeleteFbDocsService : Service
    {
        private bool isRunning = true;
        private readonly FbData fbd = FbData.Instance;
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            ThreadStart threadStart = new (DeleteFbDocs);
            Thread thread = new (threadStart);
            thread.Start();
            return base.OnStartCommand(intent, flags, startId);
        }

        private void DeleteFbDocs()
        {
            while (isRunning)
            {
                fbd.GetDocumentsWhereLessThan(Keys.CollectionKey, nameof(ChatModel.CreatedAt), DateTime.Now.AddDays(-1), OnComplete);
                Thread.Sleep(Keys.HourToMillisconds); 
            }
            StopSelf();
        }

        private void OnComplete(IQuerySnapshot qs)
        {
            foreach (IDocumentSnapshot doc in qs.Documents)
                fbd.DeleteDocument(Keys.CollectionKey, doc.Id, (task) => { });
        }

        public override IBinder? OnBind(Intent? intent)
        {
            // Not used
            return null;
        }
        public override void OnDestroy() 
        { 
            isRunning = false;
            base.OnDestroy();
        }
    }
}
