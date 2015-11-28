var ViewModel = function() {
    var self = this;

    self.ProductTabs = ko.observableArray();
    self.ProductTypes = ko.observableArray();
    self.ProductGroups = ko.observableArray();
    self.Products = ko.observableArray();

    self.ProductTabsDetails = ko.observable();
    self.ProductTypesDetails = ko.observable();
    self.ProductGroupsDetails = ko.observable();
    self.ProductDetail = ko.observable();

    self.ProductTypeNames = ko.observableArray();
    self.ProductGroupNames = ko.observableArray();

    self.newProductTab = {
        Name: ko.observable(),
        Color: ko.observable(),
        Priority: ko.observable(),
        Active: ko.observable(),
        ProductTypes: ko.observableArray()
    };

    self.newProductType = {
        Name: ko.observable(),
        Price: ko.observable(),
        Color: ko.observable(),
        ProductGroups: ko.observableArray()
    };

    self.newProductGroup = {
        Name: ko.observable(),
        Products: ko.observableArray()
    };

    self.newProduct = {
        Name: ko.observable(),
        Price: ko.observable(),
        Saleable: ko.observable(),
        ProductGroups: ko.observableArray()
    };

    self.error = ko.observable();

    var productTabsUri = '/api/producttabs/';
    var productTypesUri = '/api/producttypes/';
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
    };

    // ------ 
    function listCompare() {

        ko.utils.arrayForEach(self.ProductTypes(), function (item){
        //ko.utils.arrayForEach(koList, function (item) {
            self.ProductTabsDetails().ProductTypes.forEach(function (entry){
            //compareList.forEach(function (entry) {
                console.log("her it is: " + item.id + " " + entry);
                if (item.Id == entry) {
                    
                    self.ProductTypeNames.push(item);
                   // thisList.push(item);
                    console.log(item);
                };
            });
        });
    };
    // ------

    function getAllProductTabs() {
        ajaxHelper(productTabsUri, 'GET').done(function(data) {
            self.ProductTabs(data);
        });
    };

    function getAllProductTypes() {
        ajaxHelper(productTypesUri, 'GET').done(function(data) {
            self.ProductTypes(data);
        });
    };

    function getAllProductGroups() {
        ajaxHelper(productGroupsUri, 'GET').done(function (data) {
            self.ProductGroups(data);
        });
    };

    function getAllProducts() {
        ajaxHelper(productsUri, 'GET').done(function (data) {
            self.Products(data);
        });
    };

    self.refreshCollections = function () {
        console.log("refreshing");
        var data = self.ProductTabs.slice(0);
        self.ProductTabs([]);
        self.ProductTabs(data);
    };

    self.getProductTabsDetails = function(item) {
        ajaxHelper(productTabsUri + item.Id, 'GET').done(function(data) {
            self.ProductTabsDetails(data);
            listCompare();
        });
    };

    self.getProductTypesDetails = function(item) {
        ajaxHelper(productTypesUri + item.Id, 'GET').done(function(data) {
            self.ProductTypesDetails(data);
            //listCompare(self.ProductGroups(), self.ProductTypesDetails().ProductGroups, self.ProductGroupNames);
        });
    };

    self.getProductGroupsDetails = function(item) {
        ajaxHelper(productGroupsUri + item.Id, 'GET').done(function(data) {
            self.ProductGroupsDetails(data);
        });
    };

    self.getProductsDetail = function(item) {
        ajaxHelper(productsUri + item.Id, 'GET').done(function(data) {
            self.ProductDetail(data);
        });
    };



    var priorityExists = false;
    var ids = [];
    self.addProductTab = function (formElement) {
        ko.utils.arrayForEach(self.ProductTabs(), function(item) {
            if (item.Priority == self.newProductTab.Priority()) {
                alert("Priority must be different from existing priorities");
                priorityExists = true;
                return;
            }
        });

        
        if (priorityExists) {
            priorityExists = false;
            return;
        };

        ids = [];
        ko.utils.arrayForEach(self.newProductTab.ProductTypes(), function (item) {
            ids.push(item.Id);
        });

    

        var productTab = {
            Name: self.newProductTab.Name(),
            Priority: self.newProductTab.Priority(),
            Active: self.newProductTab.Active(),
            Color: self.newProductTab.Color(),
            ProductTypes: ids
         };

        ajaxHelper(productTabsUri, 'POST', productTab).done(function(item) {
            self.ProductTabs.push(item);
        });
    };

    self.addProductType = function (formElement) {
        ids = [];
        ko.utils.arrayForEach(self.newProductType.ProductGroups(), function (item) {
            ids.push(item.Id);
        });

        var productType = {
            Name: self.newProductType.Name(),
            Price: self.newProductType.Price(),
            Color: self.newProductType.Color(),
            ProductTypes: ids
        };

        ajaxHelper(productTypesUri, 'POST', productType).done(function (item) {
            self.ProductTypes.push(item);
        });
    };

    self.addProductGroup = function (formElement) {
        ids = [];
        ko.utils.arrayForEach(self.newProductGroup.Products(), function (item) {
            ids.push(item.Id);
        });

        var productGroup = {
            Name: self.newProductGroup.Name(),
            Products: ids
        };

        ajaxHelper(productGroupsUri, 'POST', productGroup).done(function (item) {
            self.ProductGroups.push(item);
        });
    };

    self.addProduct = function (formElement) {
        ids = [];
        ko.utils.arrayForEach(self.newProduct.ProductGroups(), function (item) {
            ids.push(item.Id);
        });

        var product = {
            Name: self.newProduct.Name(),
            Price: self.newProduct.Price(),
            Saleable: self.newProduct.Saleable(),
            ProductGroups: ids
        };

        ajaxHelper(productsUri, 'POST', product).done(function (item) {
            self.Products.push(item);
        });
    };


    self.deleteProductTab = function(item) {
        ajaxHelper(productTabsUri + item.Id, 'DELETE').done(function(data) {
            var changedIdx = self.ProductTabs.indexOf(data);
            self.ProductTabs.splice(changedIdx, 1);
            console.log(data);
            self.refreshCollections();
        });
    };

    self.deleteProductType = function (item) {
        ajaxHelper(productTypesUri + item.Id, 'DELETE').done(function (data) {
            self.ProductTypes.remove(data);
        });
    };

    self.deleteProductGroup = function (item) {
        ajaxHelper(productGroupsUri + item.Id, 'DELETE').done(function (data) {
            self.ProductGroups.remove(data);
        });
    };

    self.deleteProduct = function (item) {
        ajaxHelper(productsUri + item.Id, 'DELETE').done(function (data) {
            self.Products.remove(data);
        });
    };



    getAllProductTabs();
    getAllProductTypes();
    getAllProductGroups();
    getAllProducts();
};

ko.applyBindings(new ViewModel());