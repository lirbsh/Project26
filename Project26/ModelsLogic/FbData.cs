using Plugin.CloudFirestore;
using Project26.General;
using System.Text.RegularExpressions;

namespace Project26.ModelsLogic
{
    internal class FbData : Models.FbDataModel
    {
        public override string GetErrorMessage(string errMessage)
        {
            string retMessage;
            int end, start = errMessage.IndexOf(Keys.MessageKey);
            if (start > 0)
            {
                end = errMessage.IndexOf(Keys.ErrorsKey, start);

                string title = errMessage[(start + Keys.MessageKey.Length)..end]
                    .Replace(Keys.Apostrophe, string.Empty)
                    .Replace(Keys.Colon, string.Empty)
                    .Replace(Keys.Comma, string.Empty)
                    .Trim();
                title = string.Join(Keys.WordsDelimiter, title.Split(Keys.TitleDelimiter));
                errMessage = errMessage[(errMessage.IndexOf(Keys.ReasonKey) +
                    Keys.ReasonKey.Length)..];
                errMessage = string.Join(Keys.WordsDelimiter,
                    Regex.Split(errMessage, Keys.UpperCaseDelimiter)).Trim();
                retMessage = title + Keys.NewLine + Keys.ReasonKey +
                Keys.WordsDelimiter + errMessage[..^1];
            }
            else
                retMessage = errMessage;
            return retMessage;
        }
        public override IListenerRegistration AddSnapshotListener(string collectonName, QuerySnapshotHandler OnChange)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            return cr.AddSnapshotListener(OnChange);
        }
        public override IListenerRegistration AddSnapshotListener(string collectonName, string documentId, DocumentSnapshotHandler OnChange)
        {
            IDocumentReference dr = fs.Collection(collectonName).Document(documentId);
            return dr.AddSnapshotListener(OnChange);
        }
        public override void CreateUser(string email, string password, string name, Action<Task> OnComplete)
        {
            facl.CreateUserWithEmailAndPasswordAsync(email, password, name).ContinueWith(OnComplete);
        }
        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
        }
        public override void SignIn(string email, string password, Action<Task> OnComplete)
        {
            facl.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(OnComplete);
        }
        public override void GetDocumentsWhereEqualTo(string collectonName, string fName, object fValue, Action<IQuerySnapshot> OnComplete)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            cr.WhereEqualsTo(fName, fValue).GetAsync().ContinueWith(t => OnComplete(t.Result));
        }

        public override string SetDocument(object obj, string collectonName, string? id, Action<Task> OnComplete)
        {
            IDocumentReference dr = string.IsNullOrEmpty(id) ? fs.Collection(collectonName).Document() : fs.Collection(collectonName).Document(id);
            dr.SetAsync(obj).ContinueWith(OnComplete);
            return dr.Id;
        }
        public override void DeleteDocument(string collectonName, string id, Action<Task> OnComplete)
        {
            IDocumentReference dr = fs.Collection(collectonName).Document(id);
            dr.DeleteAsync().ContinueWith(OnComplete);
        }
        public override  void UpdateField(string collectonName, string id, string fieldName, object fieldValue, Action<Task> OnComplete)
        {
            IDocumentReference dr = fs.Collection(collectonName).Document(id);
            dr.UpdateAsync(fieldName, fieldValue).ContinueWith(OnComplete);
        }
        public override  void UpdateFields(string collectonName, string id, Dictionary<string, object> dict, Action<Task> OnComplete)
        {
            IDocumentReference dr = fs.Collection(collectonName).Document(id);
            dr.UpdateAsync(dict).ContinueWith(OnComplete);
        }
    }
}
