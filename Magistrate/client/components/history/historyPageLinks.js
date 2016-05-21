import React from 'react'
import { Link } from 'react-router'

const HistoryPageLinks = ({ historyLength, pageSize }) => {

  const totalPages = Math.ceil(historyLength / pageSize);
  var links = [];

  for (var i = 0; i < totalPages; i++) {
    links.push(<li key={i}><Link to={"/history/" + i}>{i + 1}</Link></li>);
  }

  return (
    <ul className="pagination">
      {links}
    </ul>
  )
}

export default HistoryPageLinks
