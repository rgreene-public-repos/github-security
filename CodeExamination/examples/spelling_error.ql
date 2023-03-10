/**
 * @name Checks to see if there are any variable typos
 * @description Uses a list of known misspelt words
 * @kind problem
 * @problem.severity recommendation
 * @precision high
 * @id cs/spelling-errors
 * @tags maintainability
 */

import csharp
import codeql.typos.TypoDatabase

from Variable v
where v.getFile().getAbsolutePath().matches("%TestApp%")
and typos(v.getName(), _)
select v, "Likely typo detected.", v.getName()



