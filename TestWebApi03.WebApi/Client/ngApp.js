var App = angular.module('testWebApiApp', ['ngDragDrop'])
    //.service('RoomService', function($q) {
    //  var rooms;
    //  return function () {
    //      var deferred = $q.defer();


    //      authObj.$authAnonymously().then(function(authData) {
    //          auth = authData;
    //          deferred.resolve(authData);
    //      });
    //      return deferred.promise;
    //  }
    //})
  .controller('RoomsController', function ($scope, $http, $filter, $timeout) {
      $scope.roomsList = [];
      $scope.message = "initializing...";
      $scope.strongMessage = "";
      $scope.selectedRoom = null;
      $scope.dwellers = [];

      var emptyRoom = {
          RoomName: "",
          Floor: 0,
          Location: "",
          SizeM2: 0,
          Id: 0
      }
      var emptyInhab = {
          Name: "",
          JobDescription: "",
          Title: "",
          LastName: "",
          RoomId: 0
      }

      $scope.newRoom = jQuery.extend({}, emptyRoom);
      $scope.newInhab = jQuery.extend({}, emptyInhab);

      $scope.addRoom = function () {
          $http.post("/api/Rooms", $scope.newRoom).success(function (data, status) {
              $scope.message = "Raum wurde hinzugefügt";
              $scope.newRoom = jQuery.extend({}, emptyRoom);
              console.log(JSON.stringify(data));
              //$scope.initialize();
              //{"Id":7,"RoomName":"Bar","SizeM2":133,"Floor":2,"Location":"Fantasien","Inhabitants":[]}
              $scope.roomsList.push(data);
          }).error(function (data, status) {
              $scope.message = "Beim Hinzufügen gab es ein Problem: " + JSON.stringify(data);
          });
      };

      $scope.addInhabitant = function () {
          if ($scope.selectedRoom != null)
              $scope.newInhab.RoomId = $scope.selectedRoom.Id;
          else
              $scope.newInhab.RoomId = null;
          console.log("Den Bewohner " + JSON.stringify($scope.newInhab) + " speichern und in den Raum " + JSON.stringify($scope.selectedRoom) + " aufnehmen");
          $http.post("/api/Residents", $scope.newInhab).success(function (data, status) {
              console.log("room back from insert: " + JSON.stringify(data));
              $scope.message = "Inhabitant wurde hinzugefügt";
              $scope.newInhab = jQuery.extend({}, emptyInhab);
              // data = room, or inhabitant
              if (data.Inhabitants) {
                  $filter("filter")($scope.roomsList, { Id: data.Id })[0].Inhabitants = data.Inhabitants;
                  customSuccess("Inhabitant added to Room", "Success");
              } else {
                  $scope.dwellers.push(data);
                  customSuccess("New Resident added", "Success");
              }
              //var roomsInArray = $.grep($scope.roomsList, function (e) { return e.Id === data.Id });

              //if (roomsInArray && roomsInArray.length == 0) {
              //    console.log("could not find room in list");
              //    return;
              //}
              //var roomInArray = roomsInArray[0];
              //roomInArray = data;
              //$scope.$apply();
          }).error(function (data, status) {
              $scope.message = "Es gab einen Fehler beim Hinzufügen eines Inhabitants";
              console.log("Fehler: " + JSON.stringify(data));
          });
      }

      $scope.selectRoom = function (room) {
          $scope.selectedRoom = room;

      }

      $scope.dropResident = function (room) {

      }

      $scope.deleteRoom = function () {
          console.log("Der Raum " + JSON.stringify($scope.selectedRoom) + " wird gelöscht...");
          $http.delete("/api/Rooms/" + $scope.selectedRoom.Id).success(function (data, status) {
              console.log("STATUS: " + status + " data: " + JSON.stringify(data));
              $scope.roomsList.splice($scope.roomsList.indexOf($scope.selectedRoom), 1);
              $scope.selectedRoom = null; //jQuery.extend({}, emptyInhab);
              customSuccess("Room was deleted", "Success");

          }).error(function (data, status) {
              customError("Could not delete Room", "Error");
              console.log("STATUS: " + status + " data: " + JSON.stringify(data));
          });
      }

      $scope.deleteResident = function (dweller) {
          $http.delete("/api/Residents/" + dweller.Id).success(function (data, status) {
              console.log("STATUS: " + status + " data: " + JSON.stringify(data));
              $scope.dwellers.splice($scope.dwellers.indexOf(dweller), 1);
              customSuccess("Resident was deleted", "Success!");
          }).error(function (data, status) {
              customError("Could not delete Resident", "Error");
              console.log("STATUS: " + status + " data: " + JSON.stringify(data));
          });
      }

      $scope.initialize = function () {
          $http.get("/api/Rooms").success(function (data, status, headers, config) {
              $scope.roomsList = data;
              customSuccess("Rooms loaded", "Success!");
          }).error(function (data, status, headers, config) {
              customError("Oops... something went wrong", "Error");
          });
          $http.get("/api/Residents").success(function (data, status) {
              $scope.dwellers = data;
          }).error(function (data, status) {
              customError("Could not load Inhabitants", "Error");
          });
      };
      $scope.$watch("dwellers", function () {
          $timeout(function () { activateUI(); }, 300);

      });
  });

function customError(message, title) {
    var newError = $('#alertDraft').clone(true).removeClass("hide").addClass("alert-danger");
    newError.find("strong").text(title);
    newError.find("span").text(message);
    $('#alertPlaceholder').append(newError);
}

function customSuccess(message, title) {
    var newSuccess = $('#alertDraft').clone(true).removeClass("hide").addClass("alert-success");
    newSuccess.find("strong").text(title);
    newSuccess.find("span").text(message);
    $('#alertPlaceholder').append(newSuccess);
}