(function ()
{
	var Page = function () { };

	Page.fn = Page.prototype;

	Page.fn.Init = function ()
	{
		this.$Module = $("#RegisterContainer");

		if (this.$Module.length == 0)
		{
			return;
		}

		this._CacheElements();
		this._BindEvents();
	};

	Page.fn._CacheElements = function ()
	{
		this.$Form = $("#RegistrationForm");
		this.$UserNameField = $("#LoginID");
		this.$PasswordField = $("#Password");
		this.$PasswordConfirmField = $("#PasswordConfirm");
		this.$FirstNameField = $("#FirstName");
		this.$LastNameField = $("#LastName");
		this.$EmailField = $("#Email");
		this.$PhoneField = $("#Phone");
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
				_self.DoRegistration();
			}
		});
	};

	Page.fn.FormIsValid = function ()
	{
		var bIsValid = true;

		//this.$UserNameField;
		//this.$PasswordField = $("#Password");
		//this.$PasswordConfirmField = $("#PasswordConfirm");
		//this.$FirstNameField = $("#FirstName");
		//this.$LastNameField = $("#LastName");
		//this.$EmailField = $("#Email");
		//this.$PhoneField = $("#Phone");

		// do validation
		return bIsValid;
	};

	Page.fn.DoRegistration = function ()
	{
		var _self = this;

		$.ajax({
			url: "~/API/RegisterUser".ResolveUrl(),
			type: "POST",
			dataType: "json",
			data:
			{
				LoginID: this.$UserNameField.val(),
				Password: this.$PasswordField.val(),
				FirstName: this.$FirstNameField.val(),
				LastName: this.$LastNameField.val(),
				Email: this.$EmailField.val(),
				PhoneNumber: this.$PhoneField.val(),
				GroupTier: 0
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
					_self._RegistrationSuccess();
				}
				else
				{
					_self._RegistrationError(data);
				}
			},
			error: function ()
			{
				_self._RegistrationError({ message: "Generic Errror." });
			}
		});
	};

	Page.fn._RegistrationSuccess = function ()
	{
		// redirect to authenticated page
		window.location = "~/".ResolveUrl();
	};

	Page.fn._RegistrationError = function (data)
	{
		// do something with the error
	};

	$(function ()
	{
		var RegistrationPage = new Page();
		RegistrationPage.Init();
	});

})();