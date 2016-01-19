(function (app) {
    var ListController = function ($scope, $http) {
        $scope.alert = {
            "title": "Delete Success!",
            "type": "info"
        };
        //$scope.message = "Hello";
        $http.get("/api/AdminProducts").success(function (data) {
            $scope.products = data;
           // console.log(datas);
        });
        $scope.delete = function (product) {
            $http.delete("/api/AdminProducts/" + product.ProductID).success(function () {
                removeMovieById(product.ProductID);
            });
        };
        
        var removeMovieById = function (id) {
            for (var i = 0; i < $scope.products.length; i++) {
                if($scope.products[i].ProductID == id)
                {
                    $scope.products.splice(i, 1);
                    break;
                }
            }
        };
    };
   // ListController.$inject["$scope", "$http"];
    app.controller("ListController", ListController);
}(angular.module("sportsstore")));