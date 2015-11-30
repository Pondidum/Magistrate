var ContentArea = React.createClass({

  render() {

    var actions = this.props.actions.map(function(action, index) {
      return (
        <div key={index} className="col-sm-2">{action}</div>
      );
    });

    return (
      <div>
        <div className="row">
          {actions}
          <FilterBar filterChanged={this.props.filterChanged} />
        </div>
        <div className="row">
          {this.props.children}
        </div>
      </div>
    );

  }

});
