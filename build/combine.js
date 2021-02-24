const fs = require('fs');
const path = require('path');

const slidesDir = path.join(__dirname, '..', 'slides');
const files = fs.readdirSync(slidesDir);

console.dir(files
    .filter(f => f !== 'ef-aspnet-cheat-sheet.md' && !f.endsWith('.png')));

const contents = files
    .filter(f => f !== 'ef-aspnet-cheat-sheet.md' && !f.endsWith('.png') && f !== '9999_full.md')
    .map(f => fs.readFileSync(path.join(slidesDir, f), 'utf8'));
const content = contents.join('\n\n----\n\n')
fs.writeFileSync(path.join(slidesDir, '9999_full.md'), content);
