# How to control concurrency when editing persistent objects in the ASPxGridView


<p><a href="http://msdn.microsoft.com/en-us/library/ms178472.aspx">ASP.NET page life cycle</a> is short. This makes the implementation of <a href="http://en.wikipedia.org/wiki/Optimistic_concurrency_control">optimistic concurrency control</a> in ASP.NET different than that found in Windows Forms. This example uses <a href="http://www.devexpress.com/xpo">XPO</a> and shows how to check an OptimisticLockField value prior to updating a data store.</p><p><strong>See Also:</strong><br />
<a href="https://www.devexpress.com/Support/Center/p/K18061">How to use XPO in an ASP.NET (Web) application</a><br />
<a href="http://documentation.devexpress.com/#XPO/CustomDocument2106">Optimistic Concurrency Control in XPO</a></p>

<br/>


