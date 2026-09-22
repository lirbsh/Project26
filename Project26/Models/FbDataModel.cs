using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.CloudFirestore;
using Project26.General;

namespace Project26.Models
{
    internal abstract class FbDataModel
    {
        protected FirebaseAuthClient facl;
        protected IFirestore fs = CrossCloudFirestore.Current.Instance;
        public IListenerRegistration? ilr;
        public FbDataModel()
        {
            FirebaseAuthConfig fac = new()
            {
                ApiKey = ApiKeys.FbApiKey,
                AuthDomain = ApiKeys.FbAppDomainKey,
                Providers = [new EmailProvider()]
            };
            facl = new FirebaseAuthClient(fac);        
        }
        public abstract string GetErrorMessage(string errMessage);
        public abstract void CreateUser(string email, string password, string name,Action<Task> OnComplete);
        public abstract void SignIn(string email, string password, Action<Task> OnComplete);
        public abstract IListenerRegistration AddSnapshotListener(string collectonName, Plugin.CloudFirestore.QuerySnapshotHandler OnChange);
        public abstract IListenerRegistration AddSnapshotListener(string collectonName, string documentId, Plugin.CloudFirestore.DocumentSnapshotHandler OnChange);
        public abstract void RemoveSnapshotListener();
        public abstract void GetDocumentsWhereEqualTo(string collectonName, string fName, object fValue, Action<IQuerySnapshot> OnComplete);

    }
}
