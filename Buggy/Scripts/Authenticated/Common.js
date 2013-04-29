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
		this.$SignOutLink = $("#SignOutUser");
	};

	Page.fn._BindEvents = function ()
	{
		this.$SignOutLink.on("click", function ()
		{
			document.cookie = 'Buggy_User=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
			window.location.reload();
		});
	};

	$(function ()
	{
		window.AuthenticatedPage = new Page();
		window.AuthenticatedPage.Init();
	});

})();