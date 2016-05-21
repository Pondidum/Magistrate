import React from 'react'
import { connect } from 'react-redux'

import FilterBar from '../filterbar'
import HistoryRows from './HistoryRows'
import HistoryPageLinks from './HistoryPageLinks'

const mapStateToProps = (state, ownProps) => {
  return {
    history: state.history,
    page: parseInt(ownProps.params.page) || 0
  }
}

const HistoryOverview = ({ history, page }) => {

  const pageSize = 10;

  return (
    <div>
      <div>
        <div className="row">
          <FilterBar filterChanged={() => { }} />
        </div>
        <div className="row">
          <HistoryRows history={history} pageSize={pageSize} page={page} />
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
