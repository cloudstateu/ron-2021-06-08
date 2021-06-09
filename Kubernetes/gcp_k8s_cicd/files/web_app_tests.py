import unittest
from web_app import appVersion

class TestWebApp(unittest.TestCase):

  def test_appVersion(self):
    self.assertEqual(appVersion(), "This is Python Web App - Version 1\n")

if __name__ == '__main__':
  unittest.main()
  