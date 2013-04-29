(function ()
{

	var Page = function () { };

	Page.fn = Page.prototype;

	Page.fn.Init = function ()
	{
		this._CacheElements();
		this._BindEvents();
	};

	Page.fn._CacheElements = function ()
	{
	};

	Page.fn._BindEvents = function ()
	{
		$("div.module-inner").on("click", "tr[data-bug_id]", function ()
		{
			window.location = ("~/Site/Detail?bugID=" + $(this).attr("data-bug_id")).ResolveUrl();
		});
	};

	$(function ()
	{
		window.OverviewPage = new Page();
		window.OverviewPage.Init();
	});

})();