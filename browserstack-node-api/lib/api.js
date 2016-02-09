var request = require('request'),
  log = require('./helper').log;

exports.makeRequest = function(bstackUserName, bstackAccessKey, callback) {
  var options = {
    url: 'https://www.browserstack.com/automate/browsers.json',
    auth: {
      user: bstackUserName,
      pass: bstackAccessKey
    }
  };
  request.get(options, function(err, res, body) {
    if(err) {
      log.error('Error: ' + err);
      log.error('BrowserStack request Error', 'Cannot make request to browserstack.com. Please check your network.');
      throw new Error('Network Error');
    }
    if(body.toLowerCase().indexOf('HTTP Basic: Access denied'.toLowerCase()) >= 0) {
      log.error('Auth Error', 'The credentials you provided were incorrect. Please check BROWSERSTACK_USERNAME and BROWSERSTACK_ACCESS_KEY environment variables with creds at https://www.browserstack.com/accounts/settings.');
      throw new Error('Auth Error');
    }
    var resJson = {};
    try{
      resJson = JSON.parse(body);
    } catch(err) {
      log.error('Error: ' + err);
      log.error('BrowserStack request Error', 'The response from BrowserStack was not a valid json. Please check your network connection.');
      throw new Error('BrowserStack Request Error');
    }
    callback(resJson);
  });
};
