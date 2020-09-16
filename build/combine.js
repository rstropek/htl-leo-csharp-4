const fs = require('fs');
const path = require('path');

const slidesDir = path.join(__dirname, '..', 'slides');
const files = fs.readdirSync(slidesDir);
const contents = files.map(f => fs.readFileSync(path.join(slidesDir, f), 'utf8'));
const content = contents.join('\n\n----\n\n')
fs.writeFileSync(path.join(slidesDir, '9999_full.md'), content);
