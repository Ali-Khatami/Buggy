(function ()
{
	var Page = function () { };

	Page.fn = Page.prototype;

	Page.fn.Init = function ()
	{
		this.$Module = $("#LoginContainer");

		if (this.$Module.length == 0)
		{
			return;
		}

		this._CacheElements();
		this._BindEvents();
	};

	Page.fn._CacheElements = function ()
	{
		this.$Form = $("form:first", this.Module);
		this.$UserNameField = $("#UserLogin");
		this.$PasswordField = $("#Password");
		this.$RememberCheck = $("#RememberMe");
		this.$RegisterButton = $("#DoRegister");
	};

	Page.fn._BindEvents = function ()
	{
		var _self = this;

		this.$Form.on("submit", function (e)
		{
			e.preventDefault();
			if (_self.FormIsValid())
			{
				_self.DoLogin();
			}
		});

		this.$RegisterButton.on("click", function ()
		{
			window.location = "~/Login/Register".ResolveUrl();
		});
	};

	Page.fn.FormIsValid = function ()
	{
		var bIsValid = false;

		if (this.$UserNameField.val() || this.$PasswordField.val())
		{
			bIsValid = true;
		}

		// do validation
		return bIsValid;
	};

	Page.fn.DoLogin = function ()
	{
		var _self = this;

		$.ajax({
			url: "~/API/Login".ResolveUrl(),
			type: "POST",
			dataType: "json",
			data:
			{
				loginID: this.$UserNameField.val(),
				password: this.$PasswordField.val(),
				remember: this.$RememberCheck.is(":checked")
			},
			beforeSend: function()
			{
				// show loader
			},
			complete: function()
			{
				// hide loader
			},
			success: function (data)
			{
				if (data.success)
				{
					_self._LoginSuccess();
				}
				else
				{
					_self._LoginError(data);
				}
			},
			error: function ()
			{
				_self._LoginError({ message: "Generic Errror." });
			}
		});
	};

	Page.fn._LoginSuccess = function ()
	{
		// redirect to authenticated page
		window.location = "~/".ResolveUrl();
	};

	Page.fn._LoginError = function (data)
	{
		// do something with the error
	};

	$(function ()
	{
		var LoginPage = new Page();
		LoginPage.Init();
	});

})();