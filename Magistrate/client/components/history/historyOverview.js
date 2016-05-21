import React from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import FilterBar from '../filterbar'
import HistoryRow from './HistoryRow'
import HistoryPageLinks from './HistoryPageLinks'

const mapStateToProps = (state, ownProps) => {
  return {
    history: state.history,
    page: parseInt(ownProps.params.page) || 0
  }
}

const HistoryOverview = ({ history, page }) => {

  const pageSize = 10;
  const current = moment();


  const start = page * pageSize;
  const end = start + pageSize;

  const rows = history.map(function(item, index) {
    return (<HistoryRow key={index} history={item} current={current} index={index}/>);
  }).slice(start, end);

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
            <HistoryPageLinks historyLength={history.length} pageSize={pageSize} />
          </div>
        </div>
      </div>
    </div>
  )
}

export default connect(mapStateToProps)(HistoryOverview)
