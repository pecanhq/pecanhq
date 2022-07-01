# -*- coding: utf-8 -*-

from setuptools import setup, find_packages
from os import path

parent = path.abspath(path.dirname(__file__))
with open(path.join(parent, 'README.md'), encoding='utf-8') as f:
    long_description = f.read()

setup(
    name='pecanhq',
    version='0.0.3',
    description="This client library unlocks Pecan IAM for server applications.",
    long_description=long_description,
    long_description_content_type='text/markdown',
    keywords='iam authorization authentication',
    project_urls={},
    packages=find_packages(),
    install_requires=[
        'requests',
    ],
    python_requires='>=3.5,<4',
)