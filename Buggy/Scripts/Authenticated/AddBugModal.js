(function ()
{

	var Modal = function () { };

	Modal.fn = Modal.prototype;

	Modal.fn.Init = function ()
	{
		this._CacheElements();
		this._BindEvents();
	};

	Modal.fn._CacheElements = function ()
	{
		this.$Modal = $('#NewBugModal');

		this.$BugName = $("#Name");
		this.$Type = $("#Type");
		this.$Priority = $("#Priority");
		this.$BugDescription = $("#Description");
		this.$Resolver = $("#Resolver");
		this.$Tester = $("#Tester");
	};

	Modal.fn._BindEvents = function ()
	{
		var _self = this;

		this.$Modal.on('show', function ()
		{
			$("input,textarea,select", '#NewBugModal').val("").blur();
		});

		$("#DoCreateBugButton").on("click", function ()
		{
			_self.DoAddBug();
		});

		var lookupCache = {};

		$("#Resolver, #Tester").autocomplete({
			source: function (request, response)
			{
				var term = request.term;

				if (term in lookupCache)
				{
					response(lookupCache[term]);
					return;
				}

				$.getJSON(
					"~/API/FindUser".ResolveUrl(),
					request,
					function (data, status, xhr)
					{
						lookupCache[term] = $.map(data, function (item)
						{
							return {
								label: item.Name,
								value: item.Name,
								hiddenValue: item.ID
							}
						});

						response(lookupCache[term]);
					}
				);
			},
			appendTo: "#NewBugModal",
			minLength: 0,
			delay: 0,
			select: function (event, ui)
			{
				$(this).attr("data-user_id", ui.item.hiddenValue).blur();
			}
		})
		.on("focus", function ()
		{
			$(this).val("").removeAttr("data-user_id");
		});
	};

	Modal.fn.DoAddBug = function ()
	{
		var _self = this;

		if (this._FormHasErrors())
		{
			return;
		}

		$.ajax({
			url: "~/API/AddBug".ResolveUrl(),
			type: "POST",
			data: {
				name: this.$BugName.val(),
				description: this.$BugDescription.val(),
				type: this.$Type.val(),
				priority: this.$Priority.val(),
				resolver: this.$Resolver.attr("data-user_id"),
				tester: this.$Tester.attr("data-user_id")
			},
			dataType: "json",
			success: function ()
			{
				_self.$Modal.modal('hide');
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
		window.AddBugModal = new Modal();
		window.AddBugModal.Init();
	});

})();