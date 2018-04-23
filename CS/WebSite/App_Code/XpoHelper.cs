using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

/// <summary>
/// Summary description for XpoHelper
/// </summary>
public static class XpoHelper {
    static XpoHelper() {
        CreateDefaultObjects();
    }

    public static Session GetNewSession() {
        return new Session(DataLayer);
    }

    public static UnitOfWork GetNewUnitOfWork() {
        return new UnitOfWork(DataLayer);
    }

    private readonly static object lockObject = new object();

    static IDataLayer fDataLayer;
    static IDataLayer DataLayer {
        get {
            if(fDataLayer == null) {
                lock(lockObject) {
                    fDataLayer = GetDataLayer();
                }
            }
            return fDataLayer;
        }
    }

    private static IDataLayer GetDataLayer() {
        XpoDefault.Session = null;

        InMemoryDataStore ds = new InMemoryDataStore();
        DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
        dict.GetDataStoreSchema(typeof(Article).Assembly);

        return new ThreadSafeDataLayer(dict, ds);
    }

    static void CreateDefaultObjects() {
        const int CategoryCount = 3;
        const int ArticleCount = 5;

        using(UnitOfWork uow = GetNewUnitOfWork()) {
            for(int i = 1; i <= CategoryCount; i++) {
                Category category = new Category(uow);
                category.Name = string.Format("Category {0}", i);
                for(int j = 1; j <= ArticleCount; j++) {
                    Article article = new Article(uow);
                    article.Category = category;
                    article.Name = string.Format("Article {0}-{1}", i, j);
                }
            }
            uow.CommitChanges();
        }
    }
}
