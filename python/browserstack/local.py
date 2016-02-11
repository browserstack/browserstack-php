import subprocess
from browserstack.local_binary import LocalBinary

class BrowserStackLocalError(Exception):
    def __init__(self, message):
        super(Exception, self).__init__(message)

class Local:
    def __init__(self, key, binary_path=None):
        self.key = key
        if binary_path is None:
            self.binary_path = LocalBinary().get_binary()
        else:
            self.binary_path = binary_path
        self.options = {}
        self.local_folder_path = None

    def _generate_args(self):
        optional_args = self.options.values()
        if self.local_folder_path is not None:
            final_args = [self.binary_path] + self.options.values() + ['-f', self.key, self.local_folder_path]
        else:
            final_args = [self.binary_path] + self.options.values() + [self.key]
        return final_args

    def start(self):
        self.proc = subprocess.Popen(self._generate_args(), stdout=subprocess.PIPE)
        self.stdout = self.proc.stdout
        self.stderr = self.proc.stderr
        while True:
            line = self.proc.stdout.readline()
            if 'Error:' in line.strip():
                raise BrowserStackLocalError(line)
            elif line.strip() == 'Press Ctrl-C to exit':
                break

    def verbose(self, enable=True):
        if enable == True:
            self.options['verbose'] = '-v'
        else:
            self.options.pop('verbose', None)
        return self

    def local_folder(self, path):
        self.local_folder_path = path
        return self

    def force_kill(self, enable):
        if enable == True:
            self.options['force_kill'] = '-force'
        else:
            self.options.pop('force_kill', None)
        return self

    def only_automate(self, enable):
        if enable == True:
            self.options['force_kill'] = '-onlyAutomate'
        else:
            self.options.pop('force_kill', None)
        return self

    def force_local(self, enable):
        if enable == True:
            self.options['force_local'] = '-forcelocal'
        else:
            self.options.pop('force_local', None)
        return self

    def proxy(self, host, port, username, password):
        if host is not None:
            self.options['proxy'] = "-proxyHost %s -proxyPort %s -proxyUser %s -proxyPass %s" % (host, str(port), username, password)
        else:
            self.options.pop('proxy', None)
        return self

    def local_identifier(self, identifier):
        if identifier is not None:
            self.options['local_indentifier'] = "-localIdentifier %s" % identifier

    def stop(self):
        if hasattr(self, 'proc'):
            self.proc.terminate()
