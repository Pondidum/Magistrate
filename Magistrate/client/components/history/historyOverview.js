import React from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import { Link } from 'react-router'
import FilterBar from '../filterbar'
import HistoryRow from './HistoryRow'

const mapStateToProps = (state, ownProps) => {
  return {
    history: state.history,
    page: parseInt(ownProps.params.page) || 0
  }
}

const HistoryOverview = ({ history, page }) => {

  const pageSize = 10;
  const current = moment();

  const totalPages = Math.ceil(history.length / pageSize);
  const start = page * pageSize;
  const end = start + pageSize;

  const rows = history.map(function(item, index) {
    return (<HistoryRow key={index} history={item} current={current} index={index}/>);
  }).slice(start, end);

  var links = [];

  for (var i = 0; i < totalPages; i++) {
    links.push(<li key={i}><Link to={"/history/" + i}>{i + 1}</Link></li>);
  }

  return (
    <div>
      <div>
        <div className="row">
          <FilterBar filterChanged={() => { }} />
        </div>
        <div className="row">
          <ul className="list-unstyled col-sm-12">
            {rows}
          </ul>
        </div>
        <div className="row">
          <div className="col-sm-12 text-center">
            <ul className="pagination">
              {links}
            </ul>
          </div>
        </div>
      </div>
    </div>
  )
}

export default connect(mapStateToProps)(HistoryOverview)
