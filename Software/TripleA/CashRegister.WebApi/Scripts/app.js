var ViewModel = function() {
    var self = this;

    self.ProductTabs = ko.observableArray();
    self.ProductTypes = ko.observableArray();
    self.ProductGroups = ko.observableArray();
    self.Products = ko.observableArray();

    self.ProductTabsDetails = ko.observable();

    self.error = ko.observable();

    var productTabsUri = '/api/producttabs/';
    var productTypesUri = '/api/producttabs/';
    var productGroupsUri = '/api/productgroups/';
    var productsUri = '/api/products/';

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
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

    function getAllProductTypes() {
        ajaxHelper(productTypesUri, 'GET').done(function(data) {
            self.ProductTypes(data);
        });
    }

    function getAllProductGroups() {
        ajaxHelper(productGroupsUri, 'GET').done(function (data) {
            self.ProductGroups(data);
        });
    }

    function getAllProducts() {
        ajaxHelper(productsUri, 'GET').done(function (data) {
            self.Products(data);
        });
    }

    self.getProductTabsDetails = function(item) {
        ajaxHelper(productTabsUri + item.Id, 'GET').done(function(data) {
            self.ProductTabsDetails(data);
        });
    }

    getAllProductTabs();
    getAllProductTypes();
    getAllProductGroups();
    getAllProducts();
};

ko.applyBindings(new ViewModel());