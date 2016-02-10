var browserStackTunnelWrapper = require('./lib/browserStackTunnelWrapper.js');

function BrowserStackApi() {
  var wrapper = new browserStackTunnelWrapper();

  this.addArgs = function(argument, value) {
    wrapper.addArgs(argument, value);
  };

  this.start = function(callback) {
    wrapper.start(callback);
  };

  this.stop = function(callback) {
    wrapper.stop(callback);
  };
};

module.exports = BrowserStackApi;
