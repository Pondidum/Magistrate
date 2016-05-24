import React, { Component} from 'react'
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

class HistoryOverview extends Component {

  constructor(props) {
    super(props);
    this.state = { filter: "" }
  }

  render() {
    const pageSize = 10;
    const exp = new RegExp(this.state.filter, "i")

    const history = this.props.history.filter(item => item.description.search(exp) != -1);

    return (
      <div>
        <div>
          <div className="row">
            <FilterBar filterChanged={value => this.setState({ filter: value })} />
          </div>
          <div className="row">
            <HistoryRows history={history} pageSize={pageSize} page={this.props.page} />
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
}

export default connect(mapStateToProps)(HistoryOverview)
