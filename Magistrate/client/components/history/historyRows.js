import React from 'react'
import moment from 'moment'
import HistoryRow from './HistoryRow'

const HistoryRows = ({ history, pageSize, page }) => {

  const current = moment();

  const start = page * pageSize;
  const end = start + pageSize;

  const rows = history.map(function(item, index) {
    return (<HistoryRow key={index} history={item} current={current} index={index}/>);
  }).slice(start, end);

  return (
    <ul className="list-unstyled col-sm-12">
      {rows}
    </ul>
  )
}

export default HistoryRows
