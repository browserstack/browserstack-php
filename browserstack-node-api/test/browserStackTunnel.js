var browserStackTunnel = require('../lib/browserStackTunnel'),
  ZipBinary = require('../lib/ZipBinary'),
  sinon = require('sinon'),
  chai = require('chai').should(),
  os = require('os'),
  fs = require('fs'),
  path = require('path'),
  childProcess = require('child_process');

describe('BrowserStackTunnel Module', function() {
  describe('OS platforms', function () {
    var sandBox = null,
      fsStub = null,
      platformStub = null,
      archStub = null;

    beforeEach(function() {
      sandBox = sinon.sandbox.create();

      fsStub = sandBox.stub(fs, 'existsSync').returns(true);
      platformStub = sandBox.stub(os, 'platform');
      archStub = sandBox.stub(os, 'arch');

      sandBox.stub(childProcess, 'spawn').returns({
        stdout: {
          on: sandBox.stub()
        },
        stderr: {
          on: sandBox.stub()
        },
        on: sandBox.stub()
      });
    });

    it('Selects Correct ZipBinary for linux x64', function() {
      platformStub.returns('linux');
      archStub.returns('x64');
      fsStub.returns(true);
      var tunnel = new browserStackTunnel({});
      tunnel.startTunnel();

      fs.existsSync.called.should.be.true;
      fs.existsSync.calledWith(path.join(__dirname, '../', 'bin/linux/x64/BrowserStackLocal')).should.be.true;
    });

    it('Selects Correct ZipBinary for linux ia32', function() {
      platformStub.returns('linux');
      archStub.returns('ia32');
      fsStub.returns(true);
      var tunnel = new browserStackTunnel({});
      tunnel.startTunnel();

      fs.existsSync.called.should.be.true;
      fs.existsSync.calledWith(path.join(__dirname, '../', 'bin/linux/ia32/BrowserStackLocal')).should.be.true;
    });

    it('Selects Correct ZipBinary for darwin', function() {
      platformStub.returns('darwin');
      archStub.returns('ia32');
      fsStub.returns(true);
      var tunnel = new browserStackTunnel({});
      tunnel.startTunnel();

      fs.existsSync.called.should.be.true;
      fs.existsSync.calledWith(path.join(__dirname, '../', 'bin/darwin/x64/BrowserStackLocal')).should.be.true;
    });

    it('Selects Correct ZipBinary for windows', function() {
      platformStub.returns('windows');
      archStub.returns('x32');
      fsStub.returns(true);
      var tunnel = new browserStackTunnel({});
      tunnel.startTunnel();

      fs.existsSync.called.should.be.true;
      fs.existsSync.calledWith(path.join(__dirname, '../', 'bin/win32/BrowserStackLocal.exe')).should.be.true;
    });

    afterEach(function() {
      sandBox.restore();
    });
  });
});
