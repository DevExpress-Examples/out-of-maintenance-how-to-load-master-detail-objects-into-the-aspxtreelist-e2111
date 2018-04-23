using System;
using DevExpress.Xpo;

public class Category : XPCustomObject {
    public Category(Session session)
        : base(session) {
    }

    private Guid _UniqueID;
    [Key(true)]
    public Guid UniqueID {
        get { return _UniqueID; }
        set { SetPropertyValue("UniqueID", ref _UniqueID, value); }
    }

    [Association("Category-Articles")]
    public XPCollection<Article> Articles {
        get { return GetCollection<Article>("Articles"); }
    }

    private string _Name;
    public string Name {
        get { return _Name; }
        set { SetPropertyValue("Name", ref _Name, value); }
    }
}