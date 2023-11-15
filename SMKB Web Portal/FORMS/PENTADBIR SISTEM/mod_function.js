function initDropdown(id, url, columns, bfrSend, onSucc) {
    if (bfrSend === null || bfrSend === undefined || bfrSend === "default") {
        bfrSend = function (settings) {
            // Replace {query} placeholder in data with user-entered search term
            settings.urlData.param2 = "secondvalue";
            settings.data = JSON.stringify({ q: settings.urlData.query, param2: settings.urlData.param2 });
            //searchQuery = settings.urlData.query;
            return settings;
        }
    }

    if (columns === null || columns === undefined || columns === "default") {
        columns = {
            value: 'value',      // specify which column is for data
            name: 'text'      // specify which column is for text
        }
    }

    if (onSucc === null || onSucc === undefined || onSucc === "default") {
        onSucc = function (response, curObj) {
            // Clear existing dropdown options
            var obj = $(curObj);

            var objItem = $(curObj).find('.menu');
            $(objItem).html('');

            // Add new options to dropdown
            if (response.d.length === 0) {
                $(obj.dropdown("clear"));
                return false;
            }

            //var listOptions = JSON.parse(response);
            listOptions = JSON.parse(response.d);

            $.each(listOptions, function (index, option) {
                $(objItem).append($('<div class="item" data-value="' + option[columns.value] + '">').html(option[columns.name]));
            });

            //if (searchQuery !== oldSearchQuery) {
            //$(obj).dropdown('set selected', $(curObj).find('.menu .item').first().data('value'));
            //}

            //oldSearchQuery = searchQuery;

            // Refresh dropdown
            $(obj).dropdown('refresh');
            $(obj).dropdown('show');
        }
    }

    $('#' + id).dropdown({
        fullTextSearch: true,
        apiSettings: {
            url: url,
            method: 'POST',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            cache: false,
            fields: columns,
            beforeSend: function (settings) {
                return bfrSend(settings);
            },
            onSuccess: function (response) {
                return onSucc(response, this);
            }
        }
    });
}