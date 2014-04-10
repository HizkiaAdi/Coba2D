public class ViewItem
{
	private string id;
	private string name;

	public ViewItem (string id, string name)
	{
		this.id = id;
		this.name = name;
	}

	public string ID
	{
		get{return this.id;}
	}

	public string Name
	{
		get{return this.name;}
	}
}
