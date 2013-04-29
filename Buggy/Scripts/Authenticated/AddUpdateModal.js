(function ()
{
	var Modal = function () { };

	Modal.fn = Modal.prototype;

	Modal.fn.Init = function ()
	{
		this.$Modal = $('#AddUpdateModal');
		if (this.$Modal.length == 0) { return; }
		this._CacheElements();
		this._BindEvents();
	};

	Modal.fn._CacheElements = function ()
	{
		this.$UpdateTextArea = $("#UpdateDescriptionTextArea");
	};

	Modal.fn._BindEvents = function ()
	{
		var _self = this;

		this.$Modal.on('show', function ()
		{
			$("textarea:first", _self.$Modal).val("").blur();
		});

		$("#DoAddBugUpdateButton").on("click", function ()
		{
			_self.DoAddUpdate();
		});
	};

	Modal.fn.DoAddUpdate = function ()
	{
		var _self = this;

		if (this._FormHasErrors())
		{
			return;
		}

		$.ajax({
			url: "~/API/AddUpdate".ResolveUrl(),
			type: "POST",
			data: {
				description: this.$UpdateTextArea.val(),
				bugID: window.currentBugID
			},
			dataType: "json",
			success: function (data)
			{
				if (data.success)
				{
					_self.$Modal.modal('hide');
					window.location.reload();
				}
			},
			error: function ()
			{
				alert("Unable to add bug.");
			}
		});
	};

	Modal.fn._FormHasErrors = function ()
	{
		var bHasErrors = false;


		return bHasErrors;
	};

	$(function ()
	{
		window.AddUpdateModal = new Modal();
		window.AddUpdateModal.Init();
	});

})();