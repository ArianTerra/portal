$(document).ready(function() {
    $('#skills').select2();
    $('#materials').select2({
        ajax: {
            url: '/api/materials',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    nameStartsWith: params.term,
                    page: params.page || 1
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                console.log(data.items)
                return {
                    results: data.items,
                    pagination: {
                        more: (params.page * 10) < data.items_count
                    }
                };
            }
        },
        placeholder: 'Search for materials',
        templateResult: function(data) {
            return data.name;
        },
        templateSelection: function(data) {
            return data.name;
        }
    });
});