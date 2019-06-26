import platform
import urllib.request
import os
import sys
import zipfile
import stat

class LocalBinary:
    def __init__(self):
        is_64bits = sys.maxsize > 2**32
        osname = platform.system()
        if osname == 'Darwin':
            self.http_path = "https://www.browserstack.com/browserstack-local/BrowserStackLocal-darwin-x64.zip"
        elif osname == 'Linux':
            if is_64bits:
                self.http_path = "https://www.browserstack.com/browserstack-local/BrowserStackLocal-linux-x64.zip"
            else:
                self.http_path = "https://www.browserstack.com/browserstack-local/BrowserStackLocal-linux-ia32.zip"
        else:
            self.http_path = "https://www.browserstack.com/browserstack-local/BrowserStackLocal-win32.zip"
        self.dest_parent_dir = os.path.join(os.path.expanduser('~'), 'browserstack')
        self.dest_file_path = os.path.join(self.dest_parent_dir, 'download.zip')

    def download(self, chunk_size=8192, progress_hook=None):
        urllib.request.urlretrieve(self.http_path, self.dest_file_path)

        with zipfile.ZipFile(self.dest_file_path, 'r') as z:
            for name in z.namelist():
                z.extract(name, self.dest_parent_dir)
                # There is only one file
                final_path = os.path.join(self.dest_parent_dir, name)
                st = os.stat(final_path)
                os.chmod(final_path, st.st_mode | stat.S_IXUSR)
                return final_path

    def get_binary(self):
        if not os.path.isdir(self.dest_parent_dir):
            os.mkdir(self.dest_parent_dir)
        bsfiles = [f for f in os.listdir(self.dest_parent_dir) if f.startswith('BrowserStackLocal')]
        if len(bsfiles) == 0:
            return self.download()
        else:
            return os.path.join(self.dest_parent_dir, bsfiles[0])
