var browserStackApi = require('../index.js'),
  sinon = require('sinon'),
  chai = require('chai').should();

describe('Index Module', function() {
  describe('Browserstack Auth', function () {
    this.timeout(0);

    var sandBox = null;

    beforeEach(function() {
      sandBox = sinon.sandbox.create();
    });

    it('should throw an error if BROWSERSTACK_ACCESS_KEY is not present and no key is provided', function (done) {
      sandBox.stub(process.env, 'BROWSERSTACK_ACCESS_KEY', '');
      var api = new browserStackApi();
      api.start(function(callbackValue) {
        (callbackValue instanceof Error).should.be.true;
        (callbackValue.message.toLowerCase().indexOf('child failed to start') >= 0).should.be.true;
        done();
      });
    });

    it('should not throw error if key is provided as argument instead of environment', function (done) {
      var accessKey = process.env.BROWSERSTACK_ACCESS_KEY;
      sandBox.stub(process.env, 'BROWSERSTACK_ACCESS_KEY', '');
      var api = new browserStackApi();
      api.addArgs('key', accessKey);
      api.start(function(callbackValue) {
        (callbackValue instanceof Error).should.be.false;
        api.stop(done);
      });
    });

    afterEach(function() {
      sandBox.restore();
    });
  });
});
