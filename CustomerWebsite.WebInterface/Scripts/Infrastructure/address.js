$(function () {
	var Address = function ($el) {
		this.$el = $el;
		//this.Firstname = ;
		//this.Lastname = $el.find('input[data-lastname]').val();
		//this.Streetname = $el.find('input[data-streetname]').val();
		//this.PostalCode = $el.find('input[data-postalcode]').val();
	};

	Address.prototype = {
		CopyTo: function ($destEl) {
			var dest = $destEl.Address();

			for (var i = 0; i < this.Fields.length; i++) {
				var field = this.Fields[i];
				dest[field](this[field]());
			}
		},
		Fields: ["Firstname", "Lastname", "Streetname", "PostalCode"],
		
		GetOrSet: function (fieldName, args) {
			var $input = this.$el.find('input[data-' + fieldName + ']');
			if (args.length > 0) {
				$input.val(args[0]);
				return;
			}
			return $input.val();
		},
		Firstname: function () {
			return this.GetOrSet('firstname', arguments);
		},
		Lastname: function () {
			return this.GetOrSet('lastname', arguments);
		},
		Streetname: function () {
			return this.GetOrSet('streetname', arguments);
		},
		PostalCode: function () {
			return this.GetOrSet('postalcode', arguments);
		}
	}

	$.fn.Address = function () {
		return new Address(this);
	}
});