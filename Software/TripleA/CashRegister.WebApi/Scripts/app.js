var ViewModel = function() {
    var self = this;

    self.ProductTabs = ko.observableArray();
    self.error = ko.observable();

    var productTabsUri = '/api/producttabs';

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: merhid,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function(jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllProductTabs() {
        ajaxHelper(productTabsUri, 'GET').done(function(data) {
            self.ProductTabs(data);
        });
    }

    getAllProductTabs();
};

ko.applyBindings(new viewModel());