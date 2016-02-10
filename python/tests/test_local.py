import unittest, time
from browserstack.local import Local, BrowserStackLocalError

class TestLocal(unittest.TestCase):
    def setUp(self):
        self.local = Local('sKUQixMHzMLvVtAqysUN', '/Users/sankha/browserstack/local/BrowserStackLocal')

    def tearDown(self):
        self.local.stop()

    def test_start_local(self):
        self.local.start()
        self.assertNotEqual(self.local.proc.pid, 0)

    def test_verbose(self):
        self.local.verbose(True)
        self.assertTrue('-v' in self.local._generate_args())

    def test_local_folder(self):
        self.local.local_folder('hello')
        self.assertTrue('-f' in self.local._generate_args())
        self.assertTrue('hello' in self.local._generate_args())

    def test_force_kill(self):
        self.local.force_kill(True)
        self.assertTrue('-force' in self.local._generate_args())

    def test_only_automate(self):
        self.local.only_automate(True)
        self.assertTrue('-onlyAutomate' in self.local._generate_args())

    def test_force_local(self):
        self.local.force_local(True)
        self.assertTrue('-forcelocal' in self.local._generate_args())

    def test_proxy(self):
        self.local.proxy('localhost', 2000, 'hello', 'test123')
        self.assertTrue("-proxyHost localhost -proxyPort 2000 -proxyUser hello -proxyPass test123" in self.local._generate_args())

    def test_local_identifier(self):
        self.local.local_identifier('mytunnel')
        self.assertTrue('-localIdentifier mytunnel' in self.local._generate_args())
