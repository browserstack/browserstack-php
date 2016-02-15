var browserStackTunnel = require('./lib/browserStackTunnel'),
  helper = require('./lib/helper');

function BrowserStackApi() {
  var tunnel = null,
    options = {
      key: process.env.BROWSERSTACK_ACCESS_KEY
    };

  this.addArgs = function(argument, value) {
    switch(argument) {
    case 'localIdentifier':
      options.localIdentifier = value;
      break;
    case 'key':
      options.key = value;
      break;
    case 'verbose':
      options.v = true;
      helper.log.level = value;
      break;
    case 'forcelocal':
      options.forcelocal = value;
      break;
    case 'hosts':
      options.hosts = value;
      break;
    case 'proxyHost':
      options.proxyHost = value;
      break;
    case 'proxyPort':
      options.proxyPort = value;
      break;
    case 'proxyUser':
      options.proxyUser = value;
      break;
    case 'proxyPass':
      options.proxyPass = value;
      break;
    case 'onlyAutomate':
      options.onlyAutomate = value;
      break;
    default:
      break;
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
