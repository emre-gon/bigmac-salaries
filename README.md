<p align="center">
  <a href="" rel="noopener">
 <img width=200px height=200px src="bigmac.jpg" alt="Project logo"></a>
</p>

<h3 align="center">Wage Comparison Over Big Mac Index</h3>

<div align="center">

[![Status](https://img.shields.io/badge/status-active-success.svg)]()
[![GitHub Issues](https://img.shields.io/github/issues/emre-gon/bigmac-index.svg)](https://github.com/emre-gon/bigmac-index/issues)
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/emre-gon/bigmac-index.svg)](https://github.com/emre-gon/bigmac-index/pulls)
[![License](https://img.shields.io/github/license/emre-gon/bigmac-index)](/LICENSE)

</div>

---


## Table of Contents

- [About](#about)
- [Known Issues](#issues)
- [Deployment](#deployment)
- [Tech](#tech)
- [Authors](#authors)
- [References](#references)

## About <a name = "about"></a>

This project uses data from The Economist and OECD, and <u><b>roughly</b></u> compares purchasing power of net minimum wage in different countries.


## Tech <a name = "tech"></a>

- Sqlite
- Python
- C#



## Known Issues <a name = "issues"></a>

I was using oecd average tax wedge[🔗](https://www.oecd.org/tax/tax-policy/taxing-wages-brochure.pdf) to calculate net wage from gross national minimum wage, (for countries except Turkey). This was causing purchasing power of minimum wage to look worse than it is.

Instead I started to use lowest marginal individual income tax rates in this link [🔗](https://en.wikipedia.org/wiki/List_of_countries_by_tax_rates).

If there is a better source feel free to contribute.


## Authors <a name = "authors"></a>

- [@emre-gon](https://github.com/emre-gon)

Feel free to contact me for improvements or suggestions. You may also file an issue or send in a pull request.

## References <a name = "references"></a>

- More info about The Big Mac Index: https://www.economist.com/big-mac-index
- Oecd: https://www.oecd.org/
