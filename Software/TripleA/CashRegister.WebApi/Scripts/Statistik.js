var StatistikViewModel = function() {
    var self = this;

    self.SalesOrders = ko.observableArray();

    self.SalesOrdersDetails = ko.observable();

    self.error = ko.observable;

    var salesOrderUri = '/api/salesorders';

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    };

    function getAllSalesOrders() {
        ajaxHelper(salesOrderUri, 'GET').done(function (data) {
            self.SalesOrders(data);
        });
    };

    self.getSaleOrderDetails = function(item) {
        ajaxHelper(salesOrderUri + item.ID, 'GET').done(function(data) {
            self.SalesOrdersDetails(data);
        });
    };


    getAllSalesOrders();
}

ko.applyBindings(new StatistikViewModel());