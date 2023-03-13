/**
 * @name Checks to see if empty if block
 * @description Finds if block that has no code
 * @kind problem
 * @problem.severity recommendation
 * @precision high
 * @id cs/blank-if-block
 * @tags reliability
 *       maintainability
 */

  // See original code from GitHub
  // https://codeql.github.com/docs/codeql-language-guides/basic-query-for-csharp-code/

import csharp

from IfStmt ifstmt, BlockStmt block
where ifstmt.getThen() = block and
  block.isEmpty()
select ifstmt, "Review the 'if' statement as the block is empty."

