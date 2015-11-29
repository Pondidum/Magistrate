var ContentArea = React.createClass({

  render() {

    return (
      <div className="col-sm-9">
        <FilterBar filterChanged={this.props.filterChanged} />
        <div className="row">
          {this.props.children}
        </div>
      </div>
    );

  }

});
