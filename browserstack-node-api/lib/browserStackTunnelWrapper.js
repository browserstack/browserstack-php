var browserStackTunnel = require('./browserStackTunnel');

function BrowserStackTunnelWrapper() {
  var options = {},
    tunnel = null;

  this.addArgs = function(argument, value) {
    if(argument === 'localIdentifier') {
      options.localIdentifier = value
    }
  };

  this.start = function(callback) {
    tunnel = browserStackTunnel(options);
    tunnel.start(callback);
  };

  this.stop = function() {
    tunnel.stop(callback);
  };
}

exports = BrowserStackTunnelWrapper;
