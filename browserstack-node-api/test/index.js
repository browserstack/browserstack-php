var browserStackApi = require('../index.js');

var api = new browserStackApi();
api.start(function() {
});
console.log("Session Started");
api.stop(function() {
});
console.log("Session Stopped");
setTimeout(function() {
  console.log("Quit");
}.bind(this), 9000);
