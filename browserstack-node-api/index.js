var browserStackTunnel = require('./lib/browserStackTunnel');

function BrowserStackApi() {
  var tunnel = null,
    options = {
      key: process.env.BROWSERSTACK_ACCESS_KEY
    };

  this.addArgs = function(argument, value) {
    if(argument === 'localIdentifier') {
      options.localIdentifier = value;
    } else if(argument === 'key') {
      options.key = value;
    }
  };

  this.start = function(callback) {
    tunnel = new browserStackTunnel(options);
    if(callback == null) {
      callback = function() {};
    }
    tunnel.start(callback);
  };

  this.stop = function(callback) {
    if(tunnel) {
      tunnel.stop(callback);
    }
  };
}

module.exports = BrowserStackApi;
