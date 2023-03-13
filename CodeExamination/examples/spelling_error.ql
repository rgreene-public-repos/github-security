/**
 * @name Checks to see misspelling in variable
 * @description Finds probably misspelling in code
 * @kind problem
 * @problem.severity recommendation
 * @precision high
 * @id cs/misspelling
 * @tags maintainability
 */

import csharp
import codeql.typos.TypoDatabase

from Variable v
where v.getFile().getAbsolutePath().matches("%TestApp%")
and typos(v.getName(), _)
select v, "Likely typo detected."