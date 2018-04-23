using System;
using DevExpress.Xpo;

public class Article: XPCustomObject {
    public Article(Session session)
        : base(session) { }

    private Guid _UniqueID;
    [Key(true)]
    public Guid UniqueID {
        get { return _UniqueID; }
        set { SetPropertyValue("UniqueID", ref _UniqueID, value); }
    }

    private Category _Category;
    [Association("Category-Articles")]
    public Category Category {
        get { return _Category; }
        set { SetPropertyValue("Category", ref _Category, value); }
    }

    private string _Name;
    public string Name {
        get { return _Name; }
        set { SetPropertyValue("Name", ref _Name, value); }
    }
}