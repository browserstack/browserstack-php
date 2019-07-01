import platform
import urllib.request
import os
import sys
import zipfile
import stat

class LocalBinary:
    def __init__(self):
        is_64bits = sys.maxsize > 2**32
        osname = platform.system().lower()
        if osname in ['darwin', 'linux']:
            self.http_path = 'https://www.browserstack.com/browserstack-local/BrowserStackLocal-%s-%s.zip' % (osname, 'x64' if is_64bits else 'ia32')
        else:
            self.http_path = "https://www.browserstack.com/browserstack-local/BrowserStackLocal-win32.zip"
        self.dest_parent_dir = os.path.join(os.path.expanduser('~'), 'browserstack')
        self.dest_file_path = os.path.join(self.dest_parent_dir, 'download.zip')

    def download(self, chunk_size=8192, progress_hook=None):
        urllib.request.urlretrieve(self.http_path, self.dest_file_path)

        final_path = None
        with zipfile.ZipFile(self.dest_file_path, 'r') as z:
            name = z.namelist()[0]  # There is only one file in the archive
            z.extract(name, self.dest_parent_dir)
            final_path = os.path.join(self.dest_parent_dir, name)
            st = os.stat(final_path)
            os.chmod(final_path, st.st_mode | stat.S_IXUSR)
        
        os.remove(self.dest_file_path)  # Remove the downloaded zip file
        return final_path

    def get_binary(self):
        if not os.path.isdir(self.dest_parent_dir):
            os.mkdir(self.dest_parent_dir)
        files_list = [os.path.join(self.dest_parent_dir, f) for f in os.listdir(self.dest_parent_dir) if f.startswith('BrowserStackLocal')]
        if len(files_list) == 0:
            return self.download()
        files_list.sort(key=os.path.getmtime, reverse=True)
        return files_list[0]
